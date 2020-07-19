"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// ----------------
// ACTION CREATORS
exports.actionCreators = {
    getEvents: function (startDateIndex, pageSize) { return function (dispatch, getState) {
        var appState = getState();
        if (appState &&
            appState.authentication &&
            appState.authentication.submitted) {
            return;
        }
        fetch("api/events")
            .then(function (response) { return response.json(); })
            .then(function (data) {
            dispatch({ type: 'EVENTS_RECEIVED', startDateIndex: startDateIndex, forecasts: data });
        });
        dispatch({ type: 'GET_EVENTS', startDateIndex: startDateIndex, pageSize: pageSize });
    }; }
};
// ----------------
// REDUCER
//# sourceMappingURL=EventList.js.map