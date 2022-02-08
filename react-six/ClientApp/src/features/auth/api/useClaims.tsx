import { api } from '@/lib/axios';
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

export { useClaims };
