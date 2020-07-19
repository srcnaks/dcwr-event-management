import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { authToken } from '../helpers/authHeader';


// -----------------
// STATE
export interface EventState {
    isLoading: boolean;
    isCreated: boolean;
    message: string;
}

export interface EventData {
    name: string;
    description: string;
    location: string;
    startTime: string;
    endTime: string;
}

// -----------------
// ACTIONS 

interface CreateEventRequested {
    type: 'CREATE_EVENT_REQUESTED';
}

interface EventCreated {
    type: 'EVENT_CREATED';
    id: string;
}

interface EventCreationFailed {
    type: 'CREATE_EVENT_FAILED';
    message: string;
}

interface ResetEventCreation {
    type: 'CLEAR_PAGE';
}

type KnownAction = CreateEventRequested | EventCreated | EventCreationFailed | ResetEventCreation;

// ----------------
// ACTION CREATORS

export const actionCreators = {
    postEvent: (name: string, description: string, location: string, startTime: string, endTime: string):
        AppThunkAction<KnownAction> => (dispatch, getState) => {
            const appState = getState();
            if (appState &&
                appState.event &&
                appState.event.isLoading) {
                return;
            }

            fetch(
                    `api/events`,
                    {
                        method: 'post',
                        body: JSON.stringify({
                            name: name,
                            description: description,
                            location: location,
                            //startTime: startTime,
                            //endTime: endTime
                        }),
                        headers: new Headers({
                            'Content-Type': 'application/json',
                            'Accept': 'application/json',
                            'Authorization': authToken()
                        })
                    }
                )
                .then(response => {
                    if (!response.ok) {
                        dispatch({ type: 'CREATE_EVENT_FAILED', message: "Failed!!!..." });
                    } else {
                        response.text().then(data => {
                            dispatch({ type: 'EVENT_CREATED', id: data });
                        });
                    }
                });

            dispatch({ type: 'CREATE_EVENT_REQUESTED' });
        },
    clearPage: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.event &&
            appState.event.isLoading) {
            return;
        }
        dispatch({ type: 'CLEAR_PAGE' });
    }
};

// ----------------
// REDUCER

const unloadedState: EventState = {
    isLoading: false,
    isCreated: false,
    message: ""
};

export const reducer: Reducer<EventState> = (state: EventState | undefined, incomingAction: Action): EventState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'EVENT_CREATED':
            const eventCreated = action as EventCreated;
            return {
                isLoading: false,
                isCreated:true,
                message: eventCreated.id
            };
        case 'CREATE_EVENT_FAILED':
            const eventCreationFailed = action as EventCreationFailed;
            return {
                isLoading: false,
                isCreated: false,
                message: eventCreationFailed.message
            };
        case 'CREATE_EVENT_REQUESTED':
            return {
                isLoading: true,
                isCreated: false,
                message: ""
            };
        case 'CLEAR_PAGE':
            return {
                isLoading: false,
                isCreated: false,
                message: ""
            };
    }

    return state;
};