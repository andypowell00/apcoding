import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { WeatherObject } from '../Model/WeatherObject';


interface WeatherState {
    forecast: WeatherObject;
    loading: boolean;
}


export class Weather extends React.Component<RouteComponentProps<{}>, WeatherState> {

    constructor() {
        super();
        this.state = { forecast: new WeatherObject(), loading: true };

        fetch('http://localhost:63271/api/weather/4887398')
            .then(response => response.json() as Promise<WeatherObject>)
            .then(data => {
                //console.log('data obj = ' + data);
                this.setState({ forecast: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Weather.renderForecastsTable(this.state.forecast);

        return <div>
            <h1>Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            { contents }
        </div>;
    }

    private static renderForecastsTable(forecast: WeatherObject) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp High</th>
                    <th>Temp Low</th>
                    <th>Status</th>
                    <th>Desc</th>
                    <th>City</th>
                </tr>
            </thead>
            <tbody>
            {
                <tr key={ forecast.id}>
                    <td>{ Date.now().toString()}</td>
                    <td>{ forecast.temp_max}</td>
                    <td>{ forecast.temp_min }</td>
                    <td>{ forecast.status }</td>
                    <td>{ forecast.description }</td>
                    <td>{ forecast.city }</td>
                </tr>
            }
            </tbody>
        </table>;
    }
}




    

