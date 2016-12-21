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

    if (props.isLoading) {
        return (<span>Loading ...</span>)
    }

    let isEditMode = false;
    let unitType = '';
    const setEditMode = (isEditMode) => {return false;};
    const cancelEdit = isEditMode ? <span><a onClick={setEditMode.bind(null, !isEditMode)} className="setEditMode">Cancel Edit</a><br /><br /></span> : <span />
    let isLoading = false;
    let loading = isLoading ? <span>loading...</span> : <span />

    const isIos = () => {return false;};
    let showGetMore = false;
    let getMoreDataPrompt = (isIos() && !isLoading) ? <span>Pull up to get more data</span> : <span />
    const getLocations = () => {return false;};
    let getMoreLocationsPrompt = (showGetMore && !isIos() && !isLoading) ? <a onClick={getLocations}>... get more ...</a> : <span />

    const listOfLocations = (locationData, index) => {
        return (
            <li className="list-group-item" key={index}>
                <span className="badge"><span>{locationData.currentTemperature}</span>&deg; <span>{locationData.currentUnitType}</span></span>
                <DeleteButton isEditMode={isEditMode} locations={props.locations} />
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
                    {props.locations.map(listOfLocations)}
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


