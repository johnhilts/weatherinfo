import React from 'react';

export const AddLocation = (props) => {
    return (
        <form onSubmit={props.searchLocation}>
            <div className="modal-header" style={{backgroundColor: 'white', }}>
                <h4>Add Location</h4>
            </div>
            <div className="modal-body" style={{backgroundColor: 'white', }}>
                <div style={{backgroundColor: 'white', marginBottom: '15px', }}>
                    <div>
                        <input type="text" placeholder="Enter Location Here" data-ng-model="address" autoFocus />&nbsp;
                        <button className="btn btn-success" data-toggle="modal" data-target='#addLocationModal'>Search</button>
                    </div>
                </div>
                <div>&nbsp;</div>
            </div>
        </form>
        )
}

export default AddLocation;

