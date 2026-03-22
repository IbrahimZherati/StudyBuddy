import React from 'react'
import StarRow from './StarRow';
import ReviewCard from './ReviewCard';
import CardContainer from './CardContainer';

export default function Review() {

    const reviews = [
        {
            userName: "Yara",
            userMajor: "Dentist major",
            reviewText: "I found a study partner within 10 minutes. It's great!",
            rating: 4
        },
        {
            userName: "Yara",
            userMajor: "Dentist major",
            reviewText: "I found a study partner within 10 minutes. It's great!",
            rating: 4
        },
        {
            userName: "Yara",
            userMajor: "Dentist major",
            reviewText: "I found a study partner within 10 minutes. It's great!",
            rating: 4
        },
        {
            userName: "Yara",
            userMajor: "Dentist major",
            reviewText: "I found a study partner within 10 minutes. It's great!",
            rating: 4
        }
    ]

    return (
        <section className="mx-12 my-6">
            <h3 className="text-3xl font-bold mb-2">
                Reviews
            </h3>
            
            <p className='mb-6'>
                <StarRow numOfStars={5}/> 
            </p>
            
            <CardContainer>
                {reviews.map((review, i) => (
                    <ReviewCard key={i}
                        {...review}
                    />
                ))}
            </CardContainer>
        </section>
    );
}

