import React from 'react';
import { useRecipes } from "../api";

function RecipeList() {
	const { data: recipes } = useRecipes('');
	const actualData = recipes?.data?.data; // this is dumb...

	return <>
		{
			actualData && actualData?.map((recipe) => {
				return <div key={recipe.id}>{recipe.title}</div>;
			})
		}
	</>;
}

export { RecipeList }
