import React from 'react';

export default function AddLocationButton(props) {
    return (
        <button type="button" className="btn btn-info" onClick={props.showAddLocationForm}>
            <span className="glyphicon glyphicon-plus" aria-hidden="true"></span>
        </button>
    )
}


