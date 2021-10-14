import React from 'react';
import { Route, Switch } from 'react-router';
import { Client1 } from './components/Client1';
import { Home } from './components/Home';
import './custom.css';


export default function App(){
  return (
    <Switch>
      <Route exact path='/' component={Home} />
      <Route exact path='/gc' component={Client1} />      
    </Switch>
  );
}
