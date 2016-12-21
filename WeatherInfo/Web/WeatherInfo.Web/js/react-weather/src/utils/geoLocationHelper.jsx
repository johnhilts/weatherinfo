const hasGeoLocation = navigator && navigator.geolocation;

const getEmptyLocation = () => {
    return { latitude: 0, longitude: 0, city: '', stateCode: '', countryCode: '', inputName: '', };
}

let globals = {
    externalSetAddressCallback: null,
    errorMessage: '',
    locationData: getEmptyLocation(),
};

export const getCurrentLocation = (externalSetAddressCallback) => {
    if (hasGeoLocation) {
        globals.externalSetAddressCallback = externalSetAddressCallback;
        navigator.geolocation.getCurrentPosition(
            function (position) {
                getCurrentLocationSuccess(position);
            }
            ,
            getCurrentLocationError);
    }
}

const getCurrentLocationSuccess = (position) => {
    globals.locationData.latitude = position.coords.latitude;
    globals.locationData.longitude = position.coords.longitude;
    setAddress();
}

const getCurrentLocationError = (msg) => {
    globals.errorMessage = msg;
    console.warn(msg.message);
}

const setAddress = () => {

    let geocoder = new window.google.maps.Geocoder();

    let location = new window.google.maps.LatLng(globals.locationData.latitude, globals.locationData.longitude);

    geocoder.geocode({ 'latLng': location }, setAddressCallback);
}

const setAddressCallback = (results, status) => {
    if (status == window.google.maps.GeocoderStatus.OK) {
        if (results && results[0]) {
            setAddressFields(results[0].address_components);
            if (globals.externalSetAddressCallback) {
                globals.externalSetAddressCallback(globals.locationData);
            }
        }
    }
}

const setAddressFields = (addressInfo) => {
    let addressComponentsCount = addressInfo.length;
    for (var i = 0; i < addressComponentsCount; i++) {
        let longName = addressInfo[i].long_name;
        let shortName = addressInfo[i].short_name;
        switch (addressInfo[i].types[0]) {
            case "locality":
                globals.locationData.city = longName;
                break;
            case "administrative_area_level_1":
                globals.locationData.stateCode = shortName;
                break;
            case "country":
                globals.locationData.countryCode = shortName;
                break;
        }
    }
}

export const searchLocation = (address, externalSetAddressCallback) => {
    if (hasGeoLocation) {
        globals.locationData = getEmptyLocation();
        globals.locationData.inputName = address;
        let geocoder = new window.google.maps.Geocoder();
        globals.externalSetAddressCallback = externalSetAddressCallback;
        geocoder.geocode({ 'address': address },
            function (results, status) {
                globals.setLocationByAddressCallback(results, status);
            }
            ,
            globals.getCurrentLocationError);
    }
}

