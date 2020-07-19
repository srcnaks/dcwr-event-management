import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { authToken } from '../helpers/authHeader';


// -----------------
// STATE
export interface EventState {
    isLoading: boolean;
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

type KnownAction = CreateEventRequested | EventCreated | EventCreationFailed;

// ----------------
// ACTION CREATORS

export const actionCreators = {
    postEvent: (name: string, description: string, location: string, startTime: string, endTime: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
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
            .then(response => response.text())
            .then(data => {
                dispatch({ type: 'EVENT_CREATED', id: data });
            })
            .catch(reason => dispatch({ type: 'CREATE_EVENT_FAILED', message: reason }));

        dispatch({ type: 'CREATE_EVENT_REQUESTED' });
    }
};

// ----------------
// REDUCER

const unloadedState: EventState = {
    isLoading: false,
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
                message: eventCreated.id
            };
        case 'CREATE_EVENT_FAILED':
            const eventCreationFailed = action as EventCreationFailed;
            return {
                isLoading: false,
                message: eventCreationFailed.message
            };
        case 'CREATE_EVENT_REQUESTED':
            return {
                isLoading: true,
                message: ""
            };
    }

    return state;
};