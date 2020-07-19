import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE
export interface EventsState {
    isLoading: boolean;
    startDateIndex: number;
    pageSize: number,
    events: EventData[];
}

export interface EventData {
    id: string;
    name: string;
    description: string;
    location: string;
    startTime: string;
    endTime: string;
}

// -----------------
// ACTIONS 

interface GetEventsRequested {
    type: 'GET_EVENTS';
    startDateIndex: number;
    pageSize: number;
}

interface EventsReceived {
    type: 'EVENTS_RECEIVED';
    startDateIndex: number;
    events: EventData[];
}

type KnownAction = GetEventsRequested | EventsReceived;

// ----------------
// ACTION CREATORS

export const actionCreators = {
    getEvents: (startDateIndex: number, pageSize: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.authentication &&
            appState.authentication.submitted) {
            return;
        }
        fetch(`api/events?pageNumber=${startDateIndex}&pageSize=${pageSize}`)
            .then(response => response.json() as Promise<EventData[]>)
            .then(data => {
                dispatch({ type: 'EVENTS_RECEIVED', startDateIndex: startDateIndex, events: data });
            });

        dispatch({ type: 'GET_EVENTS', startDateIndex: startDateIndex, pageSize: pageSize });
    }
};

// ----------------
// REDUCER

const unloadedState: EventsState = {
    isLoading: false,
    startDateIndex: 1,
    pageSize: 10,
    events: []
};

export const reducer: Reducer<EventsState> = (state: EventsState | undefined, incomingAction: Action): EventsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'EVENTS_RECEIVED':
            return {
                isLoading: false,
                startDateIndex: action.startDateIndex,
                pageSize: state.pageSize,
                events: action.events
            };
        case 'GET_EVENTS':
            return {
                isLoading: true,
                startDateIndex: action.startDateIndex,
                pageSize: action.pageSize,
                events: []
            };
    }

    return state;
};
