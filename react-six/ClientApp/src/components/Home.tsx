import useClaims from '@/apis/claims';
import React from 'react';

function Home() {
	const { data: claims, isLoading } = useClaims();
	let logoutUrl = claims?.find((claim: any) => claim.type === 'bff:logout_url');
	let nameDict =
		claims?.find((claim: any) => claim.type === 'name') ||
		claims?.find((claim: any) => claim.type === 'sub');
	let username = nameDict?.value;

	return (
		<>
			{isLoading ? (
				<div className="absolute top-0 left-0 z-10 flex h-full w-full items-center justify-center bg-white opacity-50">
					{/* <ImSpinner2 className="text-2xl animate-spin" /> */}
					<svg
						className="h-5 w-5 animate-spin text-gray-800"
						xmlns="http://www.w3.org/2000/svg"
						fill="none"
						viewBox="0 0 24 24"
					>
						<circle
							className="opacity-25"
							cx={12}
							cy={12}
							r={10}
							stroke="currentColor"
							strokeWidth={4}
						/>
						<path
							className="opacity-75"
							fill="currentColor"
							d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
						/>
					</svg>
				</div>
			) : (
				<div className="m-12 rounded-md border p-20">
					<Mvc username={username} logoutUrl={logoutUrl} />
				</div>
			)}
		</>
	);
}

interface MvcProps {
	username: string;
	logoutUrl: any;
}

function Mvc({ username, logoutUrl }: MvcProps) {
	return (
		<div className="">
			{!username ? (
				<a
					href="/bff/login?returnUrl=/"
					className="inline-block rounded-md border border-transparent bg-blue-500 px-4 py-2 text-center text-base font-medium text-white hover:bg-opacity-75"
				>
					Login
				</a>
			) : (
				<div className="block flex-shrink-0">
					<div className="flex items-center">
						<div className="ml-3">
							<p className="block text-base font-medium text-blue-500 md:text-sm">{`Hi, ${username}!`}</p>
							<a
								href={logoutUrl?.value}
								className="mt-1 block text-sm font-medium text-blue-200 hover:text-blue-500 md:text-xs"
							>
								Logout
							</a>
						</div>
					</div>
				</div>
			)}
		</div>
	);
}

export { Home };
