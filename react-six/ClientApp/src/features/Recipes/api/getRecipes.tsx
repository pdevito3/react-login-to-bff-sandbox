import { api } from '@/lib/axios';
import { useQuery } from 'react-query';
import { RecipeKeys } from './recipes.keys';


// const getRecipes = (queryString: string) => {
// 	return api.get(`/api/recipes?${queryString}`).then((response) => {
// 		const apiResponse = response.data as RecipeDto[];

// 		return {
// 			data: apiResponse,
// 			pagination: JSON.parse(response.headers['x-pagination']) as Pagination
// 		} as PagedApiResponse<RecipeDto>
// 	});
// };
const getRecipes = (queryString: string) => {
	return api.get(`/api/recipes?${queryString}`).then((response) => {
		const apiResponse = response.data as any;

		return {
			data: apiResponse,
			pagination: JSON.parse(response.headers['x-pagination']) as any
		} as any
	});
};

const useRecipes = (queryString: string) => {
	var qs = `pageSize=10&pageNumber=10&filters=filters&sortOrder=sortOrder`;

	return useQuery(
		RecipeKeys.list(queryString ?? ''),
		() => getRecipes(queryString)
	);
};

export { useRecipes };
