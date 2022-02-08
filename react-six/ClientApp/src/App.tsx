import React, { Component } from 'react';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';
import { Route } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { Notifications } from '@/components/Notifications';
import { Login } from './features/auth';
import './custom.css';
import 'react-toastify/dist/ReactToastify.min.css';

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<>
				<BrowserRouter>
					<QueryClientProvider client={new QueryClient()}>
						<Route exact path="/" component={Login} />
						<ReactQueryDevtools initialIsOpen={false} />
					</QueryClientProvider>
				</BrowserRouter>
				<Notifications />
			</>
		);
	}
}
