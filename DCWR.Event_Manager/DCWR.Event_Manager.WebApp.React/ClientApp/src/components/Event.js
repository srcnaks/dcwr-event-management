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
var ApplicationStore = require("../store/Event");
var FormInput_1 = require("./FormInput");
var Event = /** @class */ (function (_super) {
    __extends(Event, _super);
    function Event() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.state = {
            name: '',
            description: '',
            location: '',
            startTime: '',
            endTime: '',
            submitted: false
        };
        _this.handleChange = function (e) {
            var _a;
            var _b = e.currentTarget, name = _b.name, value = _b.value;
            _this.setState((_a = {}, _a[name] = value, _a));
        };
        _this.handleSubmit = function () {
            var _a = _this.state, name = _a.name, description = _a.description, location = _a.location, startTime = _a.startTime, endTime = _a.endTime;
            if (name && description) {
                _this.props.postEvent(name, description, location, startTime, endTime);
            }
        };
        return _this;
    }
    // This method is called when the component is first added to the document
    Event.prototype.componentDidMount = function () {
    };
    // This method is called when the route parameters change
    Event.prototype.componentDidUpdate = function () {
    };
    Event.prototype.render = function () {
        var isLoading = this.props.isLoading;
        var _a = this.state, name = _a.name, description = _a.description, location = _a.location, startTime = _a.startTime, endTime = _a.endTime, submitted = _a.submitted;
        return (React.createElement("div", { className: "col-md-6 col-md-offset-3" },
            React.createElement("h2", null, "Create Event"),
            React.createElement("div", null,
                React.createElement(FormInput_1.default, { label: "Name", name: "name", value: name, handleChange: this.handleChange }),
                React.createElement(FormInput_1.default, { label: "Description", name: "description", value: description, handleChange: this.handleChange }),
                React.createElement(FormInput_1.default, { label: "Location", name: "location", value: location, handleChange: this.handleChange }),
                React.createElement(FormInput_1.default, { label: "Start Time", name: "startTime", value: startTime, handleChange: this.handleChange }),
                React.createElement(FormInput_1.default, { label: "End Time", name: "endTime", value: endTime, handleChange: this.handleChange }),
                React.createElement("div", { className: "form-group" },
                    React.createElement("button", { className: "btn btn-primary", onClick: this.handleSubmit, disabled: isLoading }, "Create")))));
    };
    return Event;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return state.event; }, // Selects which state properties are merged into the component's props
ApplicationStore.actionCreators // Selects which action creators are merged into the component's props
)(Event);
//# sourceMappingURL=Event.js.map