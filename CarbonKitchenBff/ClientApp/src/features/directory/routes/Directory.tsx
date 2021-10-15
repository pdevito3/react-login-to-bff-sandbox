
import { Dialog, Transition } from '@headlessui/react';
import { XIcon } from '@heroicons/react/outline';
import React, { Fragment, useState } from 'react';
import { Link } from "react-router-dom";


const providers = [
  { name: 'Genesis Care',
    imageSrc: 'https://pbs.twimg.com/profile_images/1015147528625573888/gxU9EtCu.jpg',
    imageAlt: 'Genesis Care',
    description: 'We are really good at this certain thing.',
    href: '/gc' 
  },
  { name: 'OSU',
    imageSrc: 'https://d31029zd06w0t6.cloudfront.net/wp-content/uploads/sites/54/2020/11/web1_osufootball-6.jpg',
    imageAlt: 'OSU',
    description: 'We are really good at this certain thing.', 
    href: '/osu' 
  },
]

function Directory() {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false)

  return (
    <div className="">
      
      {/* Mobile menu */}
      <Transition.Root show={mobileMenuOpen} as={Fragment}>
        <Dialog as="div" className="fixed inset-0 z-40 flex lg:hidden" onClose={setMobileMenuOpen}>
          <Transition.Child
            as={Fragment}
            enter="transition-opacity ease-linear duration-300"
            enterFrom="opacity-0"
            enterTo="opacity-100"
            leave="transition-opacity ease-linear duration-300"
            leaveFrom="opacity-100"
            leaveTo="opacity-0"
          >
            <Dialog.Overlay className="fixed inset-0 bg-black bg-opacity-25" />
          </Transition.Child>

          <Transition.Child
            as={Fragment}
            enter="transition ease-in-out duration-300 transform"
            enterFrom="-translate-x-full"
            enterTo="translate-x-0"
            leave="transition ease-in-out duration-300 transform"
            leaveFrom="translate-x-0"
            leaveTo="-translate-x-full"
          >
            <div className="relative flex flex-col w-full max-w-xs pb-12 overflow-y-auto bg-white shadow-xl">
              <div className="flex px-4 pt-5 pb-2">
                <button
                  type="button"
                  className="inline-flex items-center justify-center p-2 -m-2 text-gray-400 rounded-md"
                  onClick={() => setMobileMenuOpen(false)}
                >
                  <span className="sr-only">Close menu</span>
                  <XIcon className="w-6 h-6" aria-hidden="true" />
                </button>
              </div>
            </div>
          </Transition.Child>
        </Dialog>
      </Transition.Root>
      
      <div className="relative overflow-hidden pb-72 bg-sky-700">
        <div aria-hidden="true" className="absolute inset-x-0 inset-y-0 w-full overflow-hidden transform -translate-x-1/2 left-1/2 lg:inset-y-0">
          <div className="absolute inset-0 flex">
            <div className="w-1/2 h-full" style={{backgroundColor: '#0a527b'}} />
            <div className="w-1/2 h-full" style={{backgroundColor: '#065d8c'}} />
          </div>
          <div className="relative flex justify-center">
            <svg className="flex-shrink-0" width={1750} height={308} viewBox="0 0 1750 308" xmlns="http://www.w3.org/2000/svg">
              <path d="M284.161 308H1465.84L875.001 182.413 284.161 308z" fill="#0369a1" />
              <path d="M1465.84 308L16.816 0H1750v308h-284.16z" fill="#065d8c" />
              <path d="M1733.19 0L284.161 308H0V0h1733.19z" fill="#0a527b" />
              <path d="M875.001 182.413L1733.19 0H16.816l858.185 182.413z" fill="#0a4f76" />
            </svg>
          </div>
        </div>
      </div>


      <main className="relative max-w-3xl px-4 py-16 mx-auto -mt-52 sm:px-6 sm:pt-24 sm:pb-32 lg:px-8">
        <div className="px-4 py-8 bg-white rounded-md shadow-md sm:px-6 lg:px-8">

          <div className="max-w-xl">
            <h1 className="text-3xl font-extrabold tracking-tight text-gray-900">Oncolens Portal Directory</h1>
            <p className="mt-2 text-sm text-gray-500">
              Find a collaboration provider in our directory.
            </p>
          </div>

          <div className="mt-12 space-y-2 sm:space-y-4 sm:mt-16 ">
            {providers.map((provider) => (
              <Link to={provider.href} key={provider.name} className="flex px-2 py-2 hover:bg-gray-100 hover:cursor-pointer">
                <div className="flex space-x-4 sm:min-w-0 sm:flex-1 sm:space-x-6 lg:space-x-8 ">
                  <img
                    src={provider.imageSrc}
                    alt={provider.imageAlt}
                    className="flex-none object-cover object-center w-16 h-16 rounded-md sm:w-20 sm:h-20"
                  />
                  <div className="pt-1.5 min-w-0 flex-1 sm:pt-0">
                    <h3 className="font-medium text-gray-900 text-md">
                      <a href={provider.href}>{provider.name}</a>
                    </h3>
                    <p className="text-sm text-gray-500 truncate">
                      <span>{provider.description}</span>{' '}
                    </p>
                  </div>
                </div>
              </Link>
            ))}
          </div>
        </div>
      </main>
      
    </div>
  )
}

export { Directory };

