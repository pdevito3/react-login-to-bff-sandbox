import { GenesisCareLogin } from '@/features/auth/routes/GenesisCareLogin';
import { HomeRedirect } from '@/features/auth/routes/HomeRedirect';
import { Directory } from '@/features/directory/routes/Directory';
import { Requests } from '@/features/requests/routes/Requests';
import React from 'react';
import { Route, Switch } from 'react-router';
import { AuthorizedLayout } from "./components/Layout";
import { NewRequest } from "./features/requests/routes/NewRequest";
import './index.css';


export default function App(){
  return (
    <Switch>
      <Route exact path='/directory' component={Directory} />
      <Route exact path='/gc' component={GenesisCareLogin} />
      <AuthorizedLayout>
        <Route path='/' component={HomeRedirect} />
        <Route exact path='/requests' component={Requests} />
        <Route exact path='/newrequest' component={NewRequest} />
      </AuthorizedLayout>
    </Switch>
  );
}
