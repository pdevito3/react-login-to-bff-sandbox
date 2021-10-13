import React, { useRef } from 'react';
import { useForm } from "react-hook-form";
import useClaims from '../apis/claims';
import { Auth0Lock, Auth0LockPasswordless } from 'auth0-lock'

function Home() {
	const { data: claimsResponse, isLoading } = useClaims();

	let claims = claimsResponse?.claims;
	// let logoutUrl = claims?.find(claim => claim.type === 'bff:logout_url')
	let nameDict = claims?.find(claim => claim.type === 'name') ||  claims?.find(claim => claim.type === 'sub');
	let username = nameDict?.value;

  const authref = useRef(null);
  console.log(authref.current)

	if(isLoading)
		return <div>Loading...</div>


  var lock = new Auth0LockPasswordless(
    "x9PBBz6mRYPUK7nATbMe7AcBQaxUxqRF",
    "dev-ziza5op9.us.auth0.com",
    {
      passwordlessMethod: 'code',
      ui: {
        container: authref.current
      },
      auth: {

      }
    }
  );

  var lockTwo = new Auth0LockPasswordless(
    "NRovTLnu61rA0S4UYndMQFAouZmVd0IC",
    "https://oncolens-dev.us.auth0.com/",
    // "x9PBBz6mRYPUK7nATbMe7AcBQaxUxqRF",
    // "dev-ziza5op9.us.auth0.com",
    {
      passwordlessMethod: "code",
      allowedConnections: ['email'],
      // allowSignUp: false,
      closable: false,
      configurationBaseUrl: 'https://cdn.us.auth0.com',
      auth: {
        redirectUrl: "https://localhost:4301/callback",
        params: {
          state: "test",
          scope: "openid profile email"
        },
        responseType: 'token',
      },
      ui: {
        // container: authref.current.value
      },
      theme: {
        //logo:            'YOUR LOGO HERE',
        // primaryColor:    'blue'
        logo: 'https://storage.googleapis.com/optio-incentives-public/logos/4_EPS.gif',
        primaryColor: '#607D8B',
      },
      // additionalSignUpFields: [{
      //   name: "address",
      //   placeholder: "enter your address",
      //   // The following properties are optional
      //   icon: "https://example.com/assests/address_icon.png",
      //   prefill: "street 123",
      //   validator: function(address) {
      //     return {
      //        valid: address.length >= 10,
      //        hint: "Must have 10 or more chars" // optional
      //     };
      //   }
      // },
      // {
      //   name: "full_name",
      //   placeholder: "Enter your full name"
      // }]
      }
  );

	return (		
		<>
			<div className="p-20 m-12 border rounded-md">
				<Mvc username={username} logoutUrl={"/auth/logout"}/>
			</div>
			
			<div className="p-20 m-12 border rounded-md">
				<BffForm />
			</div>

      <div ref={authref}></div>
      
      <>
        {/* {lockTwo.show()} */}
      </> 
		</>
	)
}

function BffForm() {
	const { register, handleSubmit, formState: { errors }, reset } = useForm();
  const onSubmit = async data => {
    // await login(data.email, data.password);
		alert('submission!')
    reset();
  };
	
	return (
		<div >

      <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
        <div className="sm:mx-auto sm:w-full sm:max-w-md">
          <img
            className="w-auto h-12 mx-auto"
            src="https://www.oncolens.com/wp-content/themes/oncolens/assets/images/logo-header.png" 
            alt="Oncolens Logo"
          />
          <h2 className="mt-6 text-3xl font-extrabold text-center text-gray-900">Sign in to the Oncolens Portal</h2>
        </div>
        
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
                    name="email"
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
                <button
                  type="submit"
                  className="flex justify-center w-full px-4 py-2 text-sm font-medium text-white bg-blue-600 border border-transparent rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
                >
                  Sign in
                </button>
              </div>
            </form>

            <div className="mt-6">
              <div className="relative">
                <div className="absolute inset-0 flex items-center">
                  <div className="w-full border-t border-gray-300" />
                </div>
                <div className="relative flex justify-center text-sm">
                  {/* <span className="px-2 text-gray-500 bg-white">Or continue with</span> */}
                </div>
              </div>

              <div className="inline-flex items-center justify-center w-full mx-auto mt-6">
                <p className="text-gray-500">Don't have an account?</p>
                <a className="pl-2 text-gray-500 underline hover:text-gray-600" href="#">Sign Up</a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
	)
}



function Mvc({username, logoutUrl}) {
	return (
		<div className="">
			{
				!username ? (
					<a
						href="/auth/login?returnUrl=/auth/unique"
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
									href={logoutUrl}
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

