"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var formInput = function (props) {
    return (React.createElement("div", { className: 'form-group' },
        React.createElement("label", { htmlFor: props.name }, props.label),
        React.createElement("input", { type: "text", className: "form-control", name: props.name, value: props.value, onChange: props.handleChange })));
};
exports.default = formInput;
//# sourceMappingURL=FormInput.js.map