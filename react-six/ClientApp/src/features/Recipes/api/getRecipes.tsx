import { api } from '@/lib/axios';
import { useQuery } from 'react-query';
import { RecipeKeys } from './recipes.keys';
import queryString from 'query-string'
import { QueryParams, RecipeDto } from "../types";
import { PagedResponse } from "@/Types/api";

const getRecipes = (queryString: string) => {
	return api.get(`/api/recipes?${queryString}`).then((response) => {
		const apiResponse = response.data as PagedResponse<RecipeDto>;

		return {
			data: apiResponse,
			pagination: JSON.parse(response.headers['x-pagination']) as any
		} as any
	});
};

const useRecipes = ({ pageNumber, pageSize, filters, sortOrder }: QueryParams = {}) => {
	const queryParams = queryString.stringify({ pageNumber, pageSize, filters, sortOrder });

	return useQuery(
		RecipeKeys.list(queryParams ?? ''),
		() => getRecipes(queryParams)
	);
};

export { useRecipes };
