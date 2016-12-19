import React, { Component } from 'react';
import Location from '../Components/Location';
import axios from 'axios';

class LocationContainer extends Component {
    /*
    constructor() {
        super();
    }
    */

    state = {latitude: '34.18083920000000', longitude: '-118.30896610000002', weatherData: [], };

    // NOTE: componentDidMount is used to initialize a component with server-side info
    // fore more info, see react docs: https://facebook.github.io/react/docs/component-specs.html
    componentWillMount() {
        
        console.log('componentDidMount');

        const getCurrentWeather = (latitude, longitude) => {
            return axios.get('https://192.168.1.18/api/Weather?latitude=' + latitude + '&longitude=' + longitude)
        }

        const processCurrentWeather = (result) => {
            if (result) {
                let weatherData = {
                    currentTemperature: result.data.CurrentTemperature,
                    currentUnitType: result.data.UnitType,
                    temperatureTimeText: result.data.TemperatureTimeText,
                    weatherQueryTime: result.data.WeatherQueryTime,
                    key: 976,
                };
                let weatherDataList = this.state.weatherData;
                weatherDataList.push(weatherData);
                this.setState({weatherData: weatherDataList});
            }
        }

        return getCurrentWeather(this.state.latitude, this.state.longitude)
			.then(processCurrentWeather)
			.catch(function(err) {console.warn('Error in getCurrentWeather: ', err)})
    }

    render() {
        // adding a bunch of temporary declarations
        let addSuccess = false;
        let addFail = false;

        let showModal = false;

        let modal = showModal ?
                <div modal="showModal" close="modalCancel()">
                    <div className="col-md-3"></div>
                    <add-location className="col-md-6"></add-location>
                    <div className="col-md-3"></div>
                </div>
            : <span />

        const addLocationAlertReset = () => {return false;};
            let addSuccessAlert = addSuccess ?
                    <div className="alert alert-success" role="alert">
                        <button type="button" className="close" aria-label="Close" onClick={addLocationAlertReset}><span aria-hidden="true">&times;</span></button>
                        Location saved.
                    </div>
                : <span />

        let addFailAlert = addFail ?
                <div className="alert alert-danger" role="alert">
                    <button type="button" className="close" aria-label="Close" onClick={addLocationAlertReset}><span aria-hidden="true">&times;</span></button>
                    Location save failed.
                </div>
                : <span />

        return (
        <div>
            {modal}

            {addSuccessAlert}

            {addFailAlert}

            <Location {...this.props} weatherData={this.state.weatherData} />
        </div>
      )
  }
}

export default LocationContainer;
