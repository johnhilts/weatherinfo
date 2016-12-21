import React from 'react';

export default function AddLocationButton(props) {
    return (
        <button type='button' className='btn btn-info' data-toggle='modal' data-target='#addLocationModal' onClick={props.showAddLocationForm}>
            <span className='glyphicon glyphicon-plus' aria-hidden='true'></span>
        </button>
    )
}


