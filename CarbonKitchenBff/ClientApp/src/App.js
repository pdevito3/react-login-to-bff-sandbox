import React, { Component } from 'react';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';
import { Route } from 'react-router';
import { Client1 } from './components/Client1';
import { Home } from './components/Home';
import './custom.css';


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <QueryClientProvider client={new QueryClient()}>
        <Route exact path='/' component={Home} />
        <Route exact path='/gc' component={Client1} />
        <ReactQueryDevtools initialIsOpen={false} />
      </QueryClientProvider>
    );
  }
}
