const RecipeKeys = {
  all: ['Recipes'] as const,
  lists: () => [...RecipeKeys.all, 'list'] as const,
  list: (filters: string) => [...RecipeKeys.lists(), { filters }] as const,
  details: () => [...RecipeKeys.all, 'detail'] as const,
  detail: (id: string) => [...RecipeKeys.details(), id] as const,
}

export { RecipeKeys };
