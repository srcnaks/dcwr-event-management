import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.
export interface AuthenticationState {
    userName: string;
    password: string;
    submitted: boolean;
    isLoggedIn: boolean;
}

export interface AuthenticationData {
    id: string;
    token: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.

interface USERS_AUTHENTICATION_REQUESTED {
    type: 'USERS_AUTHENTICATION_REQUESTED';
    userName: string;
    password: string;
}

interface USERS_AUTHENTICATED {
    type: 'USERS_AUTHENTICATED';
    userId:string;
    token: string;
}

interface USERS_NOT_AUTHENTICATED {
    type: 'USERS_NOT_AUTHENTICATED';
    message: string
}

type KnownAction = USERS_AUTHENTICATION_REQUESTED | USERS_AUTHENTICATED | USERS_NOT_AUTHENTICATED;

// ----------------
// ACTION CREATORS

export const actionCreators = {
    authenticate: (userName: string, password: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.authentication &&
            appState.authentication.submitted) {
            return;
        }
        fetch(
                `api/users/authenticate`,
                {
                    method: 'post',
                    body: JSON.stringify({ username: userName, password: password }),
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    })
                }
            )
            .then(response => {
                const receieved = response;
                const json = response.json();
                return json as Promise<AuthenticationData>;
            })
            .then(data => {
                dispatch({ type: 'USERS_AUTHENTICATED', userId: data.id, token: data.token });
            })
            .catch(reason => dispatch({ type: 'USERS_NOT_AUTHENTICATED', message: reason }));

        dispatch({ type: 'USERS_AUTHENTICATION_REQUESTED', userName: userName, password: password });
    },
    logout: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        localStorage.removeItem('user');
        dispatch({ type: 'USERS_NOT_AUTHENTICATED', message: "" });
    }
};

// ----------------
// REDUCER

const unloadedState: AuthenticationState = {
    userName: "",
    password: "",
    submitted: false,
    isLoggedIn: false
};

export const reducer: Reducer<AuthenticationState> = (state: AuthenticationState | undefined, incomingAction: Action): AuthenticationState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'USERS_AUTHENTICATED':
            const authenticatedAction = action as USERS_AUTHENTICATED;
            const authenticationData = {
                userId: authenticatedAction.userId,
                token: authenticatedAction.token
            };
            localStorage.setItem('user', JSON.stringify(authenticationData));
            return {
                userName: "",
                password: "",
                submitted: false,
                isLoggedIn: true
            };
        case 'USERS_NOT_AUTHENTICATED':
            localStorage.removeItem('user');
            return {
                userName: "",
                password: "",
                submitted: false,
                isLoggedIn: false
            };
        case 'USERS_AUTHENTICATION_REQUESTED':
            const requestedAction = action as USERS_AUTHENTICATION_REQUESTED;
            return {
                userName: requestedAction.userName,
                password: requestedAction.password,
                submitted: true,
                isLoggedIn: false
            };
    }

    return state;
};