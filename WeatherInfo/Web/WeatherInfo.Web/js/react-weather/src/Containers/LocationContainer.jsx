import React, { Component } from 'react';
import axios from 'axios';
import * as geoLocation from '../utils/geoLocationHelper';
import Location from '../Components/Location';

class LocationContainer extends Component {
    constructor() {
        super();

        this.setShowMoreButton = this.setShowMoreButton.bind(this);
        this.handleScroll = this.handleScroll.bind(this);
        this.getCurrentLocation = this.getCurrentLocation.bind(this);
    
        let previousUnitType = this.getPreviousUnitType("current");
        let unitType = 'F';
        if (previousUnitType) {
            unitType = this.getPreviousUnitType("current");
        }
    
        this.state = {latitude: '34.18083920000000', longitude: '-118.30896610000002', locations: [], unitType: unitType, previousUnitType: previousUnitType, };
    }

    // NOTE: componentDidMount is used to initialize a component with server-side info
    // fore more info, see react docs: https://facebook.github.io/react/docs/component-specs.html
    componentDidMount() {
        this.getCurrentLocation();

        window.addEventListener('scroll', this.handleScroll);
    }

    getCurrentLocation() {
        let isCurrent = true;
        const setAddressCallback = (locationData) => {
            let insertIndex = isCurrent ? 0 : 1;
            let locations = this.state.locations;
            locations.splice(insertIndex, 0, 
                { inputName: locationData.inputName, city: locationData.city, state: locationData.stateCode, country: locationData.countryCode, 
                    latitude: locationData.latitude, longitude: locationData.longitude, });
            this.setState({locations: locations, });
        }

        geoLocation.getCurrentLocation(setAddressCallback);
    }

    notSureWhereToPutThis() {
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
                // note: got rid of weatherData, using locations instead ... so need to fix this
                let weatherDataList = this.state.weatherData;
                weatherDataList.push(weatherData);
                this.setState({weatherData: weatherDataList});
            }
        }

        return getCurrentWeather(this.state.latitude, this.state.longitude)
			.then(processCurrentWeather)
			.catch(function(err) {console.warn('Error in getCurrentWeather: ', err)})
    }

    componentWillUnmount() {
        window.removeEventListener('scroll', this.handleScroll);
    }

    setShowMoreButton() {
        this.setState({showGetMore: (window.scrollTop() == 0), }); // TODO: is there a good way to detect a scroll bar?
    };

    getPreviousUnitType(key) {

        var previousUnitType = localStorage.getItem(key);
        if (previousUnitType && previousUnitType !== "undefined") {
            return previousUnitType;
        }
        else {
            return '';
        }
    }

    handleScroll() {
        if (document.scrollingElement.scrollTop + document.scrollingElement.scrollHeight == document.documentElement.clientHeight) {
            // this.getLocations(this.setShowMoreButton);
        }
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

            <Location {...this.props} locations={this.state.locations} />
        </div>
      )
  }
}

export default LocationContainer;
