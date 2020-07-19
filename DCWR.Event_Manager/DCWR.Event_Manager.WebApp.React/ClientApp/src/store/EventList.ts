import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE
export interface EventListState {
    isLoading: boolean;
    pageNumber: number;
    pageSize: number;
    totalCount: number;
    events: EventData[];
}

interface EventListResponse {
    paging: Paging;
    results: EventData[];
}

export interface EventData {
    id: string;
    name: string;
    description: string;
    location: string;
    startTime: string;
    endTime: string;
}

interface Paging {
    pageNumber: number;
    pageSize: number;
    totalCount: number;
}

// -----------------
// ACTIONS 

interface GetEventsRequested {
    type: 'GET_EVENTS';
    pageNumber: number;
    pageSize: number;
}

interface EventsReceived {
    type: 'EVENTS_RECEIVED';
    pageNumber: number;
    events: EventListResponse;
}

type KnownAction = GetEventsRequested | EventsReceived;

// ----------------
// ACTION CREATORS

export const actionCreators = {
    getEvents: (pageNumber: number, pageSize: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.eventList &&
            appState.eventList.isLoading) {
            return;
        }
        fetch(`api/events?pageNumber=${pageNumber}&pageSize=${pageSize}`)
            .then(response => response.json() as Promise<EventListResponse>)
            .then(data => {
                dispatch({ type: 'EVENTS_RECEIVED', pageNumber: pageNumber, events: data });
            });

        dispatch({ type: 'GET_EVENTS', pageNumber: pageNumber, pageSize: pageSize });
    }
};

// ----------------
// REDUCER

const unloadedState: EventListState = {
    isLoading: false,
    pageNumber: 1,
    pageSize: 10,
    totalCount: 0,
    events: []
};

export const reducer: Reducer<EventListState> = (state: EventListState | undefined, incomingAction: Action): EventListState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'EVENTS_RECEIVED':
            const received = action as EventsReceived;
            return {
                isLoading: false,
                pageNumber: received.events.paging.pageNumber,
                pageSize: received.events.paging.pageSize,
                totalCount: received.events.paging.totalCount,
                events: received.events.results
            };
        case 'GET_EVENTS':
            const requested = action as GetEventsRequested;
            return {
                isLoading: true,
                pageNumber: requested.pageNumber,
                pageSize: requested.pageSize,
                totalCount: 0,
                events: []
            };
    }

    return state;
};
