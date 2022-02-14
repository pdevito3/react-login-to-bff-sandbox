import React from 'react';
import { Login, useAuthUser } from './features/auth';
import './custom.css';
import 'react-toastify/dist/ReactToastify.min.css';
import { PrivateLayout, PublicLayout } from './components/Layouts';
import { RecipeList } from './features/Recipes';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {
	const { isLoggedIn } = useAuthUser();

	return (
		<div className="flex flex-col w-screen h-screen overflow-hidden antialiased">
			<BrowserRouter>
				<Routes>
					{/* private layout with private route children */}
					{isLoggedIn ? (
						<Route element={<PrivateLayout />}>
							<Route path="/" element={<RecipeList />} />
							<Route path="/test" element={<RecipeList />} />
						</Route>
					) : null}

					{/* public layout with public route children */}
					<Route element={<PublicLayout />}>
						{!isLoggedIn ? <Route path="/" element={<Login />} /> : null}
						<Route path="/login" element={<Login />} />
					</Route>
				</Routes>
			</BrowserRouter>
		</div>
	);
}

export default App;
