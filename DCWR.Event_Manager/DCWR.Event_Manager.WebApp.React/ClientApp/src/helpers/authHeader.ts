////export function authHeader() {
////    // return authorization header with jwt token
////    let user = JSON.parse(localStorage.getItem('user'));

////    if (user && user.token) {
////        return { 'Authorization': 'Bearer ' + user.token };
////    } else {
////        return {};
////    }
////}

export const authToken = () => {
    // return authorization header with jwt token
    try {
        const serializedUser = localStorage.getItem('user');
        if (serializedUser === null) {
            return "";
        }
        let user = JSON.parse(serializedUser);

        if (user && user.token) {
            return 'Bearer ' + user.token;
        } else {
            return '';
        }
    } catch (err) {
        return '';
    }
};