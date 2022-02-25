export interface QueryParams {
  pageNumber?: number;
  pageSize?: number;
  filters?: string;
  sortOrder?: string;
}

export interface RecipeDto {
  id: string;
}

export interface RecipeForManipulationDto {
  id: string;
}

export interface RecipeForCreationDto extends RecipeForManipulationDto { }
export interface RecipeForUpdateDto extends RecipeForManipulationDto { }

// need a string enum list?
// export const Status = ['Status1', 'Status2', null] as const;
// status: typeof Status[number];
