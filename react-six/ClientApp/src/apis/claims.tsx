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

function wait(ms: number) {
	return new Promise( (resolve) => {
			setTimeout(resolve, ms);
	});
}

const fetchClaims = async () =>
	axios.get('/bff/user', config)
		.then((res) => res.data);


function useClaims() {
	return useQuery(
		claimsKeys.claim,
		async () => {
				const [result] = await Promise.all([fetchClaims(), wait(500)]);
				return result;
		},
		{
			staleTime: Infinity,
			cacheTime: Infinity,
			retry: false
		}
	)
}

export { useClaims as default };
