"use strict";
////export function authHeader() {
////    // return authorization header with jwt token
////    let user = JSON.parse(localStorage.getItem('user'));
Object.defineProperty(exports, "__esModule", { value: true });
////    if (user && user.token) {
////        return { 'Authorization': 'Bearer ' + user.token };
////    } else {
////        return {};
////    }
////}
exports.authToken = function () {
    // return authorization header with jwt token
    try {
        var serializedUser = localStorage.getItem('user');
        if (serializedUser === null) {
            return "";
        }
        var user = JSON.parse(serializedUser);
        if (user && user.token) {
            return 'Bearer ' + user.token;
        }
        else {
            return '';
        }
    }
    catch (err) {
        return '';
    }
};
//# sourceMappingURL=authHeader.js.map