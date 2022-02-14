import { useAuthUser } from '@/features/auth';
import React from 'react';
import { Outlet } from 'react-router';
import { PrivateHeader } from '../Headers';
import { PrivateSideNav } from '../Navigation';

function PrivateLayout() {
	const { username, logoutUrl } = useAuthUser();

	return (
		<>
			<PrivateHeader />
			<div className="flex h-full">
				<PrivateSideNav />
				<main className="flex-1 p-4">
					<div className="">
						{!username ? (
							<a
								href="/bff/login?returnUrl=/"
								className="inline-block px-4 py-2 text-base font-medium text-center text-white bg-blue-500 border border-transparent rounded-md hover:bg-opacity-75"
							>
								Login
							</a>
						) : (
							<div className="flex-shrink-0 block">
								<div className="flex items-center">
									<div className="ml-3">
										<p className="block text-base font-medium text-blue-500 md:text-sm">{`Hi, ${username}!`}</p>
									</div>
								</div>
							</div>
						)}
					</div>
					<Outlet />
				</main>
			</div>
		</>
	);
}

export { PrivateLayout };
