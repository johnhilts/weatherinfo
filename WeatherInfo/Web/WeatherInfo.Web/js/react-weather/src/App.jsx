import React, { Component } from 'react';
import LocationContainer from './Containers/LocationContainer';

class App extends Component {
  render() {

    return (

        <div className="container body-content">
        
            <LocationContainer {...this.props} />

        </div>
    );
  }
}

export default App;
