import React from 'react';
import { Outlet } from 'react-router';
import { PrivateHeader } from '../Headers';

function PrivateLayout() {
	return (
		<>
			<PrivateHeader />
			<div className="flex h-full">
				<main className="flex-1 p-4">
					<Outlet />
				</main>
			</div>
		</>
	);
}

export { PrivateLayout };