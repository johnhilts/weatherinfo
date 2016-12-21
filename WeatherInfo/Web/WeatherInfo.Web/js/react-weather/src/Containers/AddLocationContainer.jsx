import React, { Component } from 'react';
import axios from 'axios';
import * as geoLocation from '../utils/geoLocationHelper';
import AddLocation from '../Components/AddLocation';

class AddLocationContainer extends Component {
    constructor(props) {
        super(props);

        this.searchLocation = this.searchLocation.bind(this);
        this.setLocation = this.setLocation.bind(this);
        this.modalCancel = this.modalCancel.bind(this);
        this.addLocation = this.addLocation.bind(this);
        this.addLocationAlertReset = this.addLocationAlertReset.bind(this);
        this.state = {errorMessages: '', locations: props.locations, addSuccess: false, 
            domain: '192.168.1.18', showModal: false, addFail: false, indexes: {previousSortIndex: -1, }, showGetMore: false, isLoading: true, isEditMode: false, };
    }

    // this is a duplicate!! the duplicate isn't in its own function, but it used to be
    setLocation(locationData, isCurrent) {
        // TODO: change to locations.push(location) - we will need to make the property names the same
        let insertIndex = 1; // isCurrent ? 0 : 1;
        let locations = this.state.locations;
        locations.splice(insertIndex, 0, 
            { inputName: locationData.inputName, city: locationData.city, state: locationData.stateCode, country: locationData.countryCode, 
                latitude: locationData.latitude, longitude: locationData.longitude, });
        this.setState({locations: locations, }, 
                this.getCurrentWeather(this.state.locations[insertIndex].latitude, this.state.locations[insertIndex].longitude, insertIndex));
    }

    modalCancel() {
        this.setState({showModal: false, });
    }

    searchLocation(event) {
		event.preventDefault();

        const searchLocationCallback = (locationData) => {
            // this.state.errorMessages = locationService.errorMessages;
            this.setLocation(locationData, false);
            this.modalCancel();
            this.addLocation(locationData);
        }

		let address = event.target[0];

		geoLocation.searchLocation(address, searchLocationCallback);
                        // alert("location add failed");
    }

    addLocation(locationData) {

        const postLocation = (locationData) => {
            return axios.post(`https://${this.state.domain}/api/Location`, 
                { InputName: locationData.inputName, City: locationData.city, StateCode: locationData.stateCode, CountryCode: locationData.countryCode, 
                    Latitude: locationData.latitude, Longitude: locationData.longitude, })
        }

        const postLocationCallback = (result) => {
            let addLocationAlertResetId = setInterval(this.addLocationAlertReset, 2000, 1);
            this.setState({addSuccess: true, addLocationAlertResetId: addLocationAlertResetId, });
        }

        postLocation(locationData)
            .then(postLocationCallback)
        .catch(function(err) {this.setState({addFail: true, });})

    }

    addLocationAlertReset() {
        clearInterval(this.state.addLocationAlertResetId);
        this.setState({addSuccess: false, addFail: false, addLocationAlertResetId: 0, });
    }

    render(){
        return <AddLocation searchLocation={this.searchLocation} />
    }

}

export default AddLocationContainer;
