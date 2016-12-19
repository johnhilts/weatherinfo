import React, { Component } from 'react';
import LocationContainer from './Containers/LocationContainer';

class App extends Component {
  render() {

    const showAddLocationForm = () => {return false;};
    return (

        <div className="container body-content">
            <h2>
                Weather by Location &nbsp; &nbsp;
                <button type="button" className="btn btn-info" onClick={showAddLocationForm}>
                    <span className="glyphicon glyphicon-plus" aria-hidden="true"></span>
                </button>
            </h2>
        
            <LocationContainer {...this.props} />

        </div>
    );
  }
}

export default App;
