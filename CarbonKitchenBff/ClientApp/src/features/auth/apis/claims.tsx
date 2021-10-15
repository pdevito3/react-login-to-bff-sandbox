import axios from 'axios';
import { useQuery } from 'react-query';

const claimsKeys = {
    claim: ['claims']
}

const config = {
	headers: {
		'X-CSRF': '1'
	}
}

interface GetUserResponse {
	claims: {
		type: string;
		value: string;
	}[],
	metadata: {
		type: string;
		value: string;
	}[]
}

const fetchClaims = async () =>
	axios.get('/bff/auth/getuser', config)
		.then((res) => res.data as GetUserResponse);


function useClaims() {
	return useQuery(
		claimsKeys.claim,
		async () => fetchClaims(),
		{
			staleTime: Infinity,
			cacheTime: Infinity,
			retry: false
		}
	)
}

export { useClaims as default };
