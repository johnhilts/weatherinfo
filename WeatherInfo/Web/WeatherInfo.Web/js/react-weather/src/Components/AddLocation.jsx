import React from 'react';

export const AddLocation = (props) => {
    return (
        <form onSubmit={props.searchLocation}>
            <div className="modal-header" style={{background-color: 'white', }}>
                <h4>Add Location</h4>
            </div>
            <div className="modal-body" style={{background-color: 'white', }}>
                <div style={{background-color: 'white', margin-bottom: '15px', }}>
                    <div>
                        <input type="text" placeholder="Enter Location Here" data-ng-model="address" autofocus />&nbsp;
                        <button className="btn btn-success">Search</button>
                    </div>
                </div>
                <div>&nbsp;</div>
            </div>
        </form>
        )
}


