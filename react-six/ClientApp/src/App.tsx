import React from 'react';
import { Login, useAuthUser } from './features/Auth';
import './custom.css';
import 'react-toastify/dist/ReactToastify.min.css';
import { PrivateLayout, PublicLayout } from './components/Layouts';
import { RecipeList } from './features/Recipes';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Home } from "./features/Home";

function App() {
	const { isLoggedIn } = useAuthUser();

	return (
		<div className="flex flex-col w-screen h-screen overflow-hidden antialiased">
			<BrowserRouter>
				<Routes>
					{/* private layout with private route children */}
					{isLoggedIn ? (
						<Route element={<PrivateLayout />}>
							<Route path="/" element={<Home />} />
							<Route path="/recipes" element={<RecipeList />} />
						</Route>
					) : null}

					{/* public layout with public route children */}
					<Route element={<PublicLayout />}>
						{/* // TODO do this on server side */}
						{!isLoggedIn ? <Route path="/" element={<Login />} /> : null}
						<Route path="/login" element={<Login />} />
					</Route>
				</Routes>
			</BrowserRouter>
		</div>
	);
}

export default App;
