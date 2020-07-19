import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as EventListStore from '../store/EventList';

// At runtime, Redux will merge together...
type WeatherForecastProps = EventListStore.EventListState // ... state we've requested from the Redux store
    & typeof EventListStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters

class EventList extends React.PureComponent<WeatherForecastProps> {
    // This method is called when the component is first added to the document
    componentDidMount = () => {
        this.ensureDataFetched();
    }

    handlePageChange= (pageNumber: number) => {
        this.props.getEvents(pageNumber, this.props.pageSize);
    }

    ensureDataFetched = () => {
        this.props.getEvents(this.props.pageNumber, this.props.pageSize);
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Event List</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {this.renderForecastsTable()}
                {this.renderPagination()}
            </React.Fragment>
        );
    }

    private renderForecastsTable() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Location</th>
                    <th>Start Time</th>
                    <th>End Time</th>
                </tr>
                </thead>
                <tbody>
                    {this.props.events.map((forecast: EventListStore.EventData) =>
                    <tr key={forecast.id}>
                        <td>{forecast.id}</td>
                        <td>{forecast.name}</td>
                        <td>{forecast.description}</td>
                        <td>{forecast.location}</td>
                        <td>{forecast.startTime}</td>
                        <td>{forecast.endTime}</td>
                    </tr>
                )}
                </tbody>
            </table>
        );
    }

    renderPagination = () => {
        const prevStartDateIndex = (this.props.pageNumber || 0) - 1;
        const nextStartDateIndex = (this.props.pageNumber || 0) + 1;

        return (
            <div className="d-flex justify-content-between">
                <button className='btn btn-outline-secondary btn-sm' onClick={()=>this.handlePageChange(this.props.pageNumber - 1)}>Previous</button>
                {this.props.isLoading && <span>Loading...</span>}
                <span>Page {this.props.pageNumber}</span>
                <button className='btn btn-outline-secondary btn-sm' onClick={()=>this.handlePageChange(this.props.pageNumber + 1)}>Next</button>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.eventList, // Selects which state properties are merged into the component's props
    EventListStore.actionCreators // Selects which action creators are merged into the component's props
)(EventList as any);