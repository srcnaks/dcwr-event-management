"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_redux_1 = require("react-redux");
var EventListStore = require("../store/EventList");
var EventList = /** @class */ (function (_super) {
    __extends(EventList, _super);
    function EventList() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        // This method is called when the component is first added to the document
        _this.componentDidMount = function () {
            _this.ensureDataFetched();
        };
        _this.handlePageChange = function (pageNumber) {
            _this.props.getEvents(pageNumber, _this.props.pageSize);
        };
        _this.ensureDataFetched = function () {
            _this.props.getEvents(_this.props.pageNumber, _this.props.pageSize);
        };
        _this.renderPagination = function () {
            var prevStartDateIndex = (_this.props.pageNumber || 0) - 1;
            var nextStartDateIndex = (_this.props.pageNumber || 0) + 1;
            return (React.createElement("div", { className: "d-flex justify-content-between" },
                React.createElement("button", { className: 'btn btn-outline-secondary btn-sm', onClick: function () { return _this.handlePageChange(_this.props.pageNumber - 1); } }, "Previous"),
                _this.props.isLoading && React.createElement("span", null, "Loading..."),
                React.createElement("span", null,
                    "Page ",
                    _this.props.pageNumber),
                React.createElement("button", { className: 'btn btn-outline-secondary btn-sm', onClick: function () { return _this.handlePageChange(_this.props.pageNumber + 1); } }, "Next")));
        };
        return _this;
    }
    EventList.prototype.render = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement("h1", { id: "tabelLabel" }, "Event List"),
            React.createElement("p", null, "This component demonstrates fetching data from the server and working with URL parameters."),
            this.renderForecastsTable(),
            this.renderPagination()));
    };
    EventList.prototype.renderForecastsTable = function () {
        return (React.createElement("table", { className: 'table table-striped', "aria-labelledby": "tabelLabel" },
            React.createElement("thead", null,
                React.createElement("tr", null,
                    React.createElement("th", null, "Id"),
                    React.createElement("th", null, "Name"),
                    React.createElement("th", null, "Description"),
                    React.createElement("th", null, "Location"),
                    React.createElement("th", null, "Start Time"),
                    React.createElement("th", null, "End Time"))),
            React.createElement("tbody", null, this.props.events.map(function (forecast) {
                return React.createElement("tr", { key: forecast.id },
                    React.createElement("td", null, forecast.id),
                    React.createElement("td", null, forecast.name),
                    React.createElement("td", null, forecast.description),
                    React.createElement("td", null, forecast.location),
                    React.createElement("td", null, forecast.startTime),
                    React.createElement("td", null, forecast.endTime));
            }))));
    };
    return EventList;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return state.eventList; }, // Selects which state properties are merged into the component's props
EventListStore.actionCreators // Selects which action creators are merged into the component's props
)(EventList);
//# sourceMappingURL=EventList.js.map