import React, { Component } from 'react';
import { Login } from './features/auth';
import './custom.css';
import 'react-toastify/dist/ReactToastify.min.css';
import { PrivateLayout, PublicLayout } from './components/Layouts';
import { RecipeList } from './features/Recipes';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<div className="flex flex-col w-screen h-screen overflow-hidden antialiased">
				<BrowserRouter>
					<Routes>
						{/* public layout with public route children */}
						<Route path="/" element={<PublicLayout />}>
							<Route path="/login" element={<Login />} />
						</Route>

						{/* private layout with private route children */}
						<Route path="/" element={<PrivateLayout />}>
							<Route index element={<RecipeList />} />
							{/* <Route path="/users" element={<UsersPage />} /> */}
						</Route>
					</Routes>
				</BrowserRouter>
			</div>
		);
	}
}
