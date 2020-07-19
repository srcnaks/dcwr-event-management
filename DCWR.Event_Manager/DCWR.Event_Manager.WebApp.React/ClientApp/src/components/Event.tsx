import * as React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as ApplicationStore from '../store/Event';
import FormInput from './FormInput'

// At runtime, Redux will merge together...
type ApplicationProps =
    ApplicationStore.EventState // ... state we've requested from the Redux store
    & typeof ApplicationStore.actionCreators; // ... plus action creators we've requested

class Event extends React.PureComponent<ApplicationProps> {
    public state = {
        name: '',
        description: '',
        location: '',
        startTime: '',
        endTime: '',
        submitted: false
    };

    // This method is called when the component is first added to the document
    public componentDidMount() {
    }

    // This method is called when the route parameters change
    public componentDidUpdate() {
    }

    handleChange = (e: React.FormEvent<HTMLInputElement>) => {
        const { name, value } = e.currentTarget;
        this.setState({ [name]: value });
    }

    handleSubmit = () => {
        const { name, description, location, startTime, endTime } = this.state;
        if (name && description) {
            this.props.postEvent(name, description, location, startTime, endTime);
        }
    }

    public render() {
        const { isLoading } = this.props;
        const { name, description, location, startTime, endTime, submitted } = this.state;
        return (
            <div className="col-md-6 col-md-offset-3">
                <h2>Create Event</h2>
                <div>
                    <FormInput label="Name" name="name" value={name} handleChange={this.handleChange}/>
                    <FormInput label="Description" name="description" value={description} handleChange={this.handleChange} />
                    <FormInput label="Location" name="location" value={location} handleChange={this.handleChange} />
                    <FormInput label="Start Time" name="startTime" value={startTime} handleChange={this.handleChange} />
                    <FormInput label="End Time" name="endTime" value={endTime} handleChange={this.handleChange} />
                    <div className="form-group">
                        <button className="btn btn-primary" onClick={this.handleSubmit} disabled={isLoading}>Create</button>
                    </div>
                </div>
            </div>
            );
    }
}

export default connect(
    (state: ApplicationState) => state.event, // Selects which state properties are merged into the component's props
    ApplicationStore.actionCreators // Selects which action creators are merged into the component's props
)(Event as any);