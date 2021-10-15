import useClaims from '@/features/auth/apis/claims';
import { Auth0LockPasswordless } from 'auth0-lock';
import React, { useRef } from 'react';

function GenesisCareLogin() {
	const { data: response, isLoading } = useClaims();

	let claims = response?.claims;
	let metadata = response?.metadata;
	let nameDict = claims?.find(claim => claim.type === 'name') ||  claims?.find(claim => claim.type === 'sub');
	let username = nameDict?.value;

  const authref = useRef(null);

	if(isLoading)
		return <div>Loading...</div>

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
        redirectUrl: "https://localhost:4301/bff/callback",
        params: {
          scope: "openid profile email"
        },
        responseType: 'token',
      },
      theme: {
        logo: "https://www.oncolens.com/wp-content/themes/oncolens/assets/images/logo-header.png",
        primaryColor: '#607D8B',
      },
      }
  );

	return (		
		<>
			<div className="p-20 m-12 border rounded-md">
				<Mvc username={username} 
        logoutUrl={"/bff/auth/logout"}
        loginUrl={"/bff/auth/login?returnUrl=/bff/auth/callback?onco_hospital_registration=gc"}
      />
			</div>
			
			<div className="p-20 m-12 border rounded-md">
				<h2>You have the following metdata:</h2>
        {
          metadata == null 
            ? (
              <p>none</p>
            )
            : (
              metadata.map((item, index) => (
                <div key={index} className="flex items-center justify-start pl-4">
                  <p className="font-medium whitespace-pre">{item.type}: </p>
                  <p>{item.value}</p>
                </div>
              ))
            )
        }
			</div>
      
      <>
        {/* {lockTwo.show()} */}
      </> 
		</>
	)
}
	
interface MvcProps {
  username: string | undefined;
  loginUrl: string;
  logoutUrl: string;
}

function Mvc({username, loginUrl, logoutUrl}: MvcProps) {
	return (
		<div className="">
			{
				!username ? (
					<a
            href={loginUrl}
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

export { GenesisCareLogin };

