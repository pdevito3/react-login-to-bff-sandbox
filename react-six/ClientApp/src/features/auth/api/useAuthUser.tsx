import { api } from '@/lib/axios';
import { useEffect, useState } from 'react';
import { useQuery } from 'react-query';

const claimsKeys = {
	claim: ['claims'],
};

function wait(ms: number) {
	return new Promise((resolve) => {
		setTimeout(resolve, ms);
	});
}

const fetchClaims = async () => api.get('/bff/user').then((res) => res.data);

function useClaims() {
	return useQuery(
		claimsKeys.claim,
		async () => {
			// const [result] = await Promise.all([fetchClaims(), wait(500)]);
			// return result;
			return fetchClaims();
		},
		{
			staleTime: Infinity,
			cacheTime: Infinity,
			retry: false,
		}
	);
}

function useAuthUser() {
	const { data: claims, isLoading } = useClaims();

	// TODO abstract to function that takes a sid
	let logoutUrl = claims?.find((claim: any) => claim.type === 'bff:logout_url');
	let nameDict =
		claims?.find((claim: any) => claim.type === 'name') ||
		claims?.find((claim: any) => claim.type === 'sub');
	let username = nameDict?.value;

	const [isLoggedIn, setIsLoggedIn] = useState(false);
	useEffect(() => {
		setIsLoggedIn(!!username);
	}, [username]);

	return {
		username,
		logoutUrl,
		isLoading,
		isLoggedIn,
	};
}

export { useAuthUser };
