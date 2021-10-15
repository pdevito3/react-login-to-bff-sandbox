import React from 'react';
import { Redirect, Route } from 'react-router';

function HomeRedirect() {
  const isAuthenticated = true;

  return (
    <Route>
      {isAuthenticated ? (
        <Redirect from="/" to="/requests"/>
      ) : (
        <Redirect from="/" to="/directory"/>
      )}
    </Route>
  )
}

export { HomeRedirect };
