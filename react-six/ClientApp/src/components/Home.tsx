import React from 'react';
import { useForm } from "react-hook-form";
import useClaims from '../apis/claims';

function Home() {
	const { data: claims, isLoading } = useClaims();
	let logoutUrl = claims?.find((claim: any) => claim.type === 'bff:logout_url')
	let nameDict = claims?.find((claim: any) => claim.type === 'name') ||  claims?.find((claim: any) => claim.type === 'sub');
	let username = nameDict?.value;

	if(isLoading)
		return <div>Loading...</div>

	return (		
		<>
			<div className="p-20 m-12 border rounded-md">
				<Mvc username={username} logoutUrl={logoutUrl}/>
			</div>
			
			<div className="p-20 m-12 border rounded-md">
				<BffForm />
			</div>
		</>
	)
}

function BffForm() {
	const { register, handleSubmit, formState: { errors }, reset } = useForm();
  const onSubmit = async (data: any) => {
    // await login(data.email, data.password);
		alert('submission!')
    reset();
  };
	
	return (
		<div >

      <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
        <div className="px-4 py-8 bg-white shadow sm:rounded-lg sm:px-10">
    			<form className="space-y-6"  onSubmit={handleSubmit(onSubmit)}>
            <div>
              <label htmlFor="email" className="block text-sm font-medium text-gray-700">
                Email address
              </label>
              <div className="mt-1">
                <input
                  id="email"
                  type="email"
                  autoComplete="email"
                  required
									{...register("email", {
										required: "required",
										pattern: {
											value: /\S+@\S+\.\S+/,
											message: "Entered value does not match email format"
										}
									})}
                  className="block w-full px-3 py-2 placeholder-gray-400 border border-gray-300 rounded-md shadow-sm appearance-none focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                />
              </div>
            </div>

            <div>
              <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                Password
              </label>
              <div className="mt-1">
                <input
                  id="password"
                  type="password"
                  autoComplete="current-password"
                  required
									{...register("password", {
										required: "required",
										minLength: {
											value: 1,
											message: "min length is 1"
										}
									})}
                  className="block w-full px-3 py-2 placeholder-gray-400 border border-gray-300 rounded-md shadow-sm appearance-none focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                />
              </div>
            </div>

            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <input
                  id="remember-me"
                  type="checkbox"
									{...register("remember-me")}
                  className="w-4 h-4 text-blue-600 border-gray-300 rounded focus:ring-blue-500"
                />
                <label htmlFor="remember-me" className="block ml-2 text-sm text-gray-900">
                  Remember me
                </label>
              </div>

              <div className="text-sm">
                <a href="#" className="font-medium text-blue-600 hover:text-blue-500">
                  Forgot your password?
                </a>
              </div>
            </div>

            <div>
              <button
                type="submit"
                className="flex justify-center w-full px-4 py-2 text-sm font-medium text-white bg-blue-600 border border-transparent rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
              >
                Sign in
              </button>
            </div>
          </form>

          </div>
      </div>
    </div>
	)
}

interface MvcProps {
  username: string;
  logoutUrl: any;
}

function Mvc({username, logoutUrl}: MvcProps) {
	return (
		<div className="">
			{
				!username ? (
					<a
						href="/bff/login?returnUrl=/"
						className="inline-block px-4 py-2 text-base font-medium text-center text-white bg-blue-500 border border-transparent rounded-md hover:bg-opacity-75"
					>
						Login
					</a>
				) : (
					<div className="flex-shrink-0 block">
						<div className="flex items-center">
							<div className="ml-3">
								<p className="block text-base font-medium text-blue-500 md:text-sm">{`Hi, ${username}!`}</p>
								<a
									href={logoutUrl?.value}
									className="block mt-1 text-sm font-medium text-blue-200 hover:text-blue-500 md:text-xs"
								>
									Logout
								</a>
							</div>
						</div>
					</div>
				)
			}
		</div>
	)

}

export { Home };

