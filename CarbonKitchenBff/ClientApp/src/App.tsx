import { Client1 } from '@/features/auth/routes/Client1';
import { Home } from '@/features/auth/routes/Home';
import { Requests } from '@/features/requests/routes/Requests';
import React from 'react';
import { Route, Switch } from 'react-router';
import { AuthorizedLayout } from "./components/Layout";
import { NewRequest } from "./features/requests/routes/NewRequest";
import './index.css';


export default function App(){
  return (
    <Switch>
      <Route exact path='/' component={Home} />
      <Route exact path='/gc' component={Client1} />
      <AuthorizedLayout>
        <Route exact path='/requests' component={Requests} />
        <Route exact path='/newrequest' component={NewRequest} />
      </AuthorizedLayout>
    </Switch>
  );
}
