import React, { Component } from 'react';
import { Route } from 'react-router';
import { Home } from './components/Home';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      // <Layout>
        <>
        <Route exact path='/' component={Home} />
        {/*<Route path='/counter' component={Counter} />*/}
        {/*<Route path='/fetch-data' component={FetchData} />*/}
        </>
      // </Layout>
    );
  }
}
