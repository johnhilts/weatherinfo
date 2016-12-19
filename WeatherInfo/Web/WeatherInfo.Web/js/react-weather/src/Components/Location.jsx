import React from 'react';

const DeleteButton = (props) => {
    const removeLocation = (locationData) => {return false;};

    if (props.isEditMode) {
        return (
            <span>
                <button type="button" className="btn btn-danger" onClick={removeLocation.bind(null, props.locationData)}>
                    <span className="glyphicon glyphicon-minus" aria-hidden="true"></span>
                </button>
                &nbsp;
            </span>
        )
    }

    return <span />
}

const Location = (props) => {

    let isEditMode = false;
    let unitType = '';
    let locations = 
        [
            {inputName : 'Test 1', city : 'Testville', state : 'NV', country : 'US', temperatureTimeText : '123 minutes ago', key : 123, },
            {inputName : 'Test 2', city : 'Testville', state : 'NV', country : 'US', temperatureTimeText : '124 minutes ago', key : 124, },
            {inputName : 'Test 3', city : 'Testville', state : 'NV', country : 'US', temperatureTimeText : '125 minutes ago', key : 125, },
            {inputName : 'Test 4', city : 'Testville', state : 'NV', country : 'US', temperatureTimeText : '126 minutes ago', key : 126, },
        ];
    locations.push(props.weatherData[0]);
    const setEditMode = (isEditMode) => {return false;};
    const cancelEdit = isEditMode ? <span><a onClick={setEditMode.bind(null, !isEditMode)} className="setEditMode">Cancel Edit</a><br /><br /></span> : <span />
    let isLoading = false;
    let loading = isLoading ? <span>loading...</span> : <span />
    const isIos = () => {return false;};
    let showGetMore = false;
    let getMoreDataPrompt = (isIos() && !isLoading) ? <span>Pull up to get more data</span> : <span />
    const getLocations = () => {return false;};
    let getMoreLocationsPrompt = (showGetMore && !isIos() && !isLoading) ? <a onClick={getLocations}>... get more ...</a> : <span />
    let temperature = '80';

    const listOfLocations = (locationData) => {
        return (
            <li className="list-group-item" key={locationData.key}>
                <span className="badge"><span>{temperature}</span>&deg; <span>{unitType}</span></span>
                <DeleteButton isEditMode={isEditMode} locations={locations} />
                <span>{locationData.inputName} ({locationData.city}), {locationData.state}&nbsp;{locationData.country}</span>
                <br /><span>(as of {locationData.temperatureTimeText})</span>
            </li>
        )
    }

    return (
            <div>
                <input type="hidden" data-ng-model="unitType" />
                <div>
                    {cancelEdit}
                    <ul className="list-group">
                        {locations.map(listOfLocations)}
                    </ul>
                    {cancelEdit}
                    {loading}
                    {getMoreDataPrompt}
                    {getMoreLocationsPrompt}
                </div>
            </div>
        )
}

export default Location;


