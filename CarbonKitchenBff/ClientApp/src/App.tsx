import { Client1 } from '@/features/auth/routes/Client1';
import { Home } from '@/features/auth/routes/Home';
import React from 'react';
import { Route, Switch } from 'react-router';
import './index.css';


export default function App(){
  return (
    <Switch>
      <Route exact path='/' component={Home} />
      <Route exact path='/gc' component={Client1} />      
    </Switch>
  );
}
