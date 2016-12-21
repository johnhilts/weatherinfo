import React, { Component } from 'react';
import axios from 'axios';
import * as geoLocation from '../utils/geoLocationHelper';
import Location from '../Components/Location';
import AddLocationButton from '../Components/AddLocationButton';

class LocationContainer extends Component {
    constructor() {
        super();

        this.setShowMoreButton = this.setShowMoreButton.bind(this);
        this.handleScroll = this.handleScroll.bind(this);
        this.getCurrentLocation = this.getCurrentLocation.bind(this);
        this.getLocations = this.getLocations.bind(this);
        this.getCurrentWeather = this.getCurrentWeather.bind(this);
        this.updateQueryTimeText = this.updateQueryTimeText.bind(this);
        this.showAddLocationForm = this.showAddLocationForm.bind(this);
        this.modalOpen = this.modalOpen.bind(this);
    
        let previousUnitType = this.getPreviousUnitType("current");
        let unitType = 'F';
        if (previousUnitType) {
            unitType = this.getPreviousUnitType("current");
        }

        // NOTE: temporarily turning this off because it interferes with hot reload
        // window.setInterval(this.updateQueryTimeText, 10000);

        this.state = {locations: [], unitType: unitType, previousUnitType: previousUnitType, 
            domain: '192.168.1.18', showModal: false, addFail: false, indexes: {previousSortIndex: -1, }, showGetMore: false, isLoading: true, isEditMode: false, };
    }

    // NOTE: componentDidMount is used to initialize a component with server-side info
    // fore more info, see react docs: https://facebook.github.io/react/docs/component-specs.html
    componentDidMount() {
        this.getCurrentLocation();
        this.getLocations(this.setShowMoreButton);

        window.addEventListener('scroll', this.handleScroll);
    }

    getCurrentLocation() {
        const setAddressCallback = (locationData) => {
            let insertIndex = 0; // isCurrent ? 0 : 1;
            let locations = this.state.locations;
            locations.splice(insertIndex, 0, 
                { inputName: "Current Location", city: locationData.city, state: locationData.stateCode, country: locationData.countryCode, 
                    latitude: locationData.latitude, longitude: locationData.longitude, });
            this.setState({locations: locations, }, 
                this.getCurrentWeather(this.state.locations[insertIndex].latitude, this.state.locations[insertIndex].longitude, insertIndex));
        }

        geoLocation.getCurrentLocation(setAddressCallback);
    }

    getLocations(successCallback) {

        // $scope.startFeedback();

        const queryLocations = (previousSortIndex) => {
            return axios.get(`https://${this.state.domain}/api/Location?previousSortOrder=${previousSortIndex}`)
        }

        const processLocations = (result) => {
            if (result) {

                const setLocations = (locationData) => { // HACK
                    // TODO: change to locations.push(location) - we will need to make the property names the same
                    this.state.locations.push(
                        { inputName: locationData.InputName, city: locationData.City, state: locationData.StateCode, country: locationData.CountryCode, 
                            latitude: locationData.Latitude, longitude: locationData.Longitude, sortOrder: locationData.sortOrder, temperatureTimeText: "...", });
                    if (this.state.indexes.previousSortIndex == -1 || locationData.SortOrder < this.state.indexes.previousSortIndex) {
                        this.state.indexes.previousSortIndex = locationData.SortOrder;
                    }
                }

                for (let i = 0; i < result.data.length; i++) {
                    setLocations(result.data[i]);
                }
            }

            const getWeatherForLocation = (locationData, locationIndex) => {
                this.getCurrentWeather(locationData.latitude, locationData.longitude, locationIndex);
            }

            const getWeatherForAllLocations = () => {
                this.state.locations.map(getWeatherForLocation);
                this.setState({isLoading: false, });
            }

            this.setState({locations: this.state.locations, }, getWeatherForAllLocations);
        }

        queryLocations(this.state.indexes.previousSortIndex)
			.then(processLocations)
			.catch(function(err) {console.warn('Error in queryLocations: ', err)})
    }

    getCurrentWeather(latitude, longitude, locationIndex) {
        const queryCurrentWeather = (latitude, longitude) => {
            return axios.get(`https://${this.state.domain}/api/Weather?latitude=${latitude}&longitude=${longitude}`)
        }

        const processCurrentWeather = (result) => {
            if (result) {
                let weatherData = {
                    currentTemperature: result.data.CurrentTemperature,
                    currentUnitType: result.data.UnitType,
                    temperatureTimeText: result.data.TemperatureTimeText,
                    weatherQueryTime: result.data.WeatherQueryTime,
                };
                this.state.locations[locationIndex].currentTemperature = weatherData.currentTemperature;
                this.state.locations[locationIndex].currentUnitType = weatherData.currentUnitType;
                this.state.locations[locationIndex].temperatureTimeText = weatherData.temperatureTimeText;
                this.state.locations[locationIndex].weatherQueryTime = weatherData.weatherQueryTime;
                this.setState({locations: this.state.locations, });
            }
        }

        queryCurrentWeather(latitude, longitude)
			.then(processCurrentWeather)
			.catch(function(err) {console.warn('Error in queryCurrentWeather: ', err)})
    }

    updateQueryTimeText() {
        
        const getQueryTimeText = (queryTime) => {
            return axios.get(`https://${this.state.domain}/api/Common/QueryTimeText?queryTime=${queryTime}`)
        }

        const getQueryTimeTextSuccess = (i, result) => {
            this.state.locations[i].temperatureTimeText = result.data;
            this.setState({locations: this.state.locations, });
        }

        for (let i = 0; i < this.state.locations.length; i++) {
            getQueryTimeText(this.state.locations[i].weatherQueryTime)
            .then(getQueryTimeTextSuccess.bind(null, i))
            .catch(
                function () {
                    // don't do anything ... it's just a text update, not important if it fails
                }
            );
        }
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

    modalOpen() {
        this.setState({showModal: true});
    }

    // this is a duplicate!!
    /*
    addLocationAlertReset() {
        clearInterval(this.state.addLocationAlertResetId);
        this.setState({addSuccess: false, addFail: false, addLocationAlertResetId: 0, });
    }
    */

    showAddLocationForm() {
        // this.addLocationAlertReset();
        this.modalOpen();
    }

    render() {
        // adding a bunch of temporary declarations
        let addSuccess = false;
        let addFail = false;

        let modal = this.state.showModal ?
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
            <h2>
                Weather by Location &nbsp; &nbsp;
                <AddLocationButton showAddLocationForm={this.showAddLocationForm} />
            </h2>

            {modal}

            {addSuccessAlert}

            {addFailAlert}

            <Location {...this.props} locations={this.state.locations} isLoading={this.state.isLoading} />
        </div>
      )
  }
}

export default LocationContainer;
