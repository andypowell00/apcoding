import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface EventState {
    events: Events[];
    loading: boolean;
}

export class Event extends React.Component<RouteComponentProps<{}>, EventState> {
    constructor() {
        super();
        this.state = { events: [], loading: true };

        fetch('http://localhost:63271/api/events/keyword=chicago')
            .then(response => response.json() as Promise<Events[]>)
            .then(data => {
                console.log('data obj = ' + data);
                this.setState({ events: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Event.renderEventsTable(this.state.events);

        return <div>
            <h1>Events</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>;
    }

    private static renderEventsTable(events: Events[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>id</th>
                    <th>type</th>
                    <th>name</th>
                    <th>url</th>
                </tr>
            </thead>
            <tbody>
                {events.map(event =>
                    <tr key={event.id}>
                        <td>{event.id}</td>
                        <td>{event.type}</td>
                        <td>{event.name}</td>
                        <td>{event.url}</td>
                        
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

interface Events {
    id: string;
    name: string;
    type: string;
    url: string;
}
