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
var ApplicationStore = require("../store/Authentication");
var LoginPage = /** @class */ (function (_super) {
    __extends(LoginPage, _super);
    function LoginPage() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.state = {
            username: '',
            password: '',
            submitted: false
        };
        _this.handleChange = function (e) {
            var _a;
            var _b = e.currentTarget, name = _b.name, value = _b.value;
            _this.setState((_a = {}, _a[name] = value, _a));
        };
        _this.handleLogin = function () {
            var _a = _this.state, username = _a.username, password = _a.password;
            if (username && password) {
                _this.props.authenticate(username, password);
            }
        };
        _this.handleLogout = function () {
            _this.props.logout();
        };
        return _this;
    }
    // This method is called when the component is first added to the document
    LoginPage.prototype.componentDidMount = function () {
    };
    // This method is called when the route parameters change
    LoginPage.prototype.componentDidUpdate = function () {
    };
    LoginPage.prototype.render = function () {
        var isLoggedIn = this.props.isLoggedIn;
        var _a = this.state, username = _a.username, password = _a.password, submitted = _a.submitted;
        return (React.createElement("div", { className: "col-md-6 col-md-offset-3" },
            React.createElement("h2", null, "Login"),
            isLoggedIn
                ? React.createElement("button", { className: "btn btn-primary", onClick: this.handleLogout }, "Logout")
                : React.createElement("div", null,
                    React.createElement("div", { className: 'form-group' + (submitted && !username ? ' has-error' : '') },
                        React.createElement("label", { htmlFor: "username" }, "Username"),
                        React.createElement("input", { type: "text", className: "form-control", name: "username", value: username, onChange: this.handleChange }),
                        submitted &&
                            !username &&
                            React.createElement("div", { className: "help-block" }, "Username is required")),
                    React.createElement("div", { className: 'form-group' + (submitted && !password ? ' has-error' : '') },
                        React.createElement("label", { htmlFor: "password" }, "Password"),
                        React.createElement("input", { type: "password", className: "form-control", name: "password", value: password, onChange: this.handleChange }),
                        submitted &&
                            !password &&
                            React.createElement("div", { className: "help-block" }, "Password is required")),
                    React.createElement("div", { className: "form-group" },
                        React.createElement("button", { className: "btn btn-primary", onClick: this.handleLogin }, "Login")))));
    };
    return LoginPage;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return state.authentication; }, // Selects which state properties are merged into the component's props
ApplicationStore.actionCreators // Selects which action creators are merged into the component's props
)(LoginPage);
//# sourceMappingURL=LoginPage.js.map