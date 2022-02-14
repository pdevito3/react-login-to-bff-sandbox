import '@/custom.css';
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';
import { Notifications } from '@/components/Notifications';
import './custom.css';
import 'react-toastify/dist/ReactToastify.min.css';

ReactDOM.render(
	<React.StrictMode>
		<QueryClientProvider client={new QueryClient()}>
			<App />
			<ReactQueryDevtools initialIsOpen={false} />
		</QueryClientProvider>
		<Notifications />
	</React.StrictMode>,
	document.getElementById('root')
);

reportWebVitals();
