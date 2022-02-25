
import { api } from "@/lib/axios";
import { useQuery } from 'react-query';
import { RecipeKeys } from "./recipes.keys";
import { RecipeDto } from '../types/index';

// export const getRecipe = (id: string) => {
// 	return api
// 		.get(`/api/events/${id}`)
// 		.then((response) => (response.data as ApiResponse<RecipeDto>).data);
// };
export const getRecipe = (recipeId: string) => {
	return api
		.get(`/api/recipes/${recipeId}`)
		.then((response) => response.data as RecipeDto);
};

export const useGetRecipe = (recipeId: string) => {
	return useQuery(RecipeKeys.detail(recipeId), () => getRecipe(recipeId));
};
