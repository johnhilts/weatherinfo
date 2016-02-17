function GeoLocator() {
	this.hasGeoLocation = navigator && navigator.geolocation;
	this.Location = 0;
	this.ErrorMessage = "";
	/*
	   this.LatitudeInput = $("#Latitude");
	   this.LongitudeInput = $("#Longitude");
	   this.AddressInfo = "";
	   this.StreetAddress = $("#StreetAddress");
	//this.StreetAddress2 = $("#StreetAddress2");
	this.City = $("#City");
	this.StateCode = $("#StateCode");
	this.PostalCode = $("#PostalCode");
	this.CountryCode = $("#CountryCode");
	$("#updateButton").click(this.setLocationByAddress.bind(this));
	*/
}

GeoLocator.prototype.getCurrentLocation =
function () {
	if (this.hasGeoLocation) {
		navigator.geolocation.getCurrentPosition(this.getCurrentLocationSuccess.bind(this), this.getCurrentLocationError.bind(this));
	}
};

GeoLocator.prototype.getCurrentLocationSuccess =
function (position) {
	this.Location = position;
	this.LatitudeInput.val(this.Location.coords.latitude);
	this.LongitudeInput.val(this.Location.coords.longitude);
	var hasAddress = $("#HasAddress");
	//if (hasAddress.val() === "False") {
	this.setAddress();
	//}
};

GeoLocator.prototype.getCurrentLocationError =
function (msg) {
	this.ErrorMessage = msg;
};

// TODO: this code has a dependency on Google maps api .... need to segregate this in case we need to disable or switch out
GeoLocator.prototype.setAddress =
function (latitude, longitude) {

	var geocoder = new google.maps.Geocoder();

	var location = new google.maps.LatLng(latitude, longitude);

	geocoder.geocode({ 'latLng': location }, this.setAddressCallback.bind(this));
};

GeoLocator.prototype.setAddressCallback =
function (results, status) {
	if (status == google.maps.GeocoderStatus.OK) {
		if (results && results[0]) {
			var output =  "";
			var addressInfo = results[0].address_components;
			var addressComponentsCount = addressInfo.length;
			for (var i = 0; i < addressComponentsCount; i++) {
				output += "<b>" + addressInfo[i].types[0] + "</b><br />" + addressInfo[i].long_name + "<br />" + addressInfo[i].short_name + "<br /><br />";
				/*
				switch (addressInfo[i].types[0]) {
					case "street_number":
						streetNumber = longName;
						break;
					case "route":
						streetName = longName;
						break;
					case "locality":
						city = longName;
						break;
					case "administrative_area_level_1":
						state = shortName;
						break;
					case "country":
						country = shortName;
						break;
					case "postal_code":
						zip = longName;
						break;
				}
				*/
			}
			// $("span").text(unescape(output));
			document.getElementsByTagName("span")[0].innerHTML = output;
		}
	}
};

// TODO: this code has a dependency on Google maps api .... need to segregate this in case we need to disable or switch out
GeoLocator.prototype.setLocationByAddress =
function (input) {
	var geocoder = new google.maps.Geocoder();

	var address = input;

	geocoder.geocode({ 'address': address }, this.setLocationByAddressCallback.bind(this));

	return false;
};

GeoLocator.prototype.setLocationByAddressCallback =
function (results, status) {
	if (status == google.maps.GeocoderStatus.OK) {
		if (results[0]) {
			var latitude = results[0].geometry.location.lat();
			var longitude = results[0].geometry.location.lng();
			this.setAddress(latitude, longitude);
		}
	}
};



